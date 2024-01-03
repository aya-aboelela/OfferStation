import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { AdminDeliveryService } from 'src/app/services/admin/Delivery/admin-delivery.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { delivery } from 'src/app/sharedClassesAndTypes/delivery';
import { DataTableDirective } from 'angular-datatables';

@Component({
  selector: 'app-admin-delivery',
  templateUrl: './admin-delivery.component.html',
  styleUrls: ['./admin-delivery.component.css']
})
export class AdminDeliveryComponent {

  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;

  Deliveries: delivery[] = [];

  delivery: delivery = {
    id: 0,
    name: '',
    phone: '',
  };

  errorMessage: any;
  display = '';
  display1 = '';
  index!: any;

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  deliveryForm: FormGroup;

  constructor(
    private _orderService: OrdersService,
    private _deliveryService: AdminDeliveryService,
    private fb: FormBuilder,
  ) {
    this.deliveryForm = this.fb.group({
      name: ['', [Validators.required, Validators.min(3), Validators.max(30)]],
      phone: ['', [Validators.required, Validators.pattern(/^01\d{9}$/)]]
    });

    this.deliveryForm.get('name')?.valueChanges.subscribe((data) => {
      this.delivery.name = data;
    });
    this.deliveryForm.get('phone')?.valueChanges.subscribe((data) => {
      this.delivery.phone = data;
    });
  }

  get name() {
    return this.deliveryForm.get('name');
  }
  get phone() {
    return this.deliveryForm.get('phone');
  }

  ngOnInit(): void {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [5, 10, 20],
      processing: true,
      destroy: true, 
      responsive: true,
      language: {
        search: '_INPUT_',
        searchPlaceholder: 'Search records',
      }
    }
    this.getDeliveries();
  }
  getDeliveries(): void {
    this._orderService.GetAllDeliveres()
      .subscribe(response => {
        this.Deliveries = response.data
        console.log("categories: ", this.Deliveries);
        this.dtTrigger.next(null);
      });
  }

  onDelete(deliveryId: number, index: number) {
    this._deliveryService.DeleteDelivery(deliveryId).subscribe({
      next: data => {
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.Deliveries.splice(index, 1);
        this.getDeliveries();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  OnSubmit() {
    this._deliveryService.AddDelivery(this.deliveryForm.value).subscribe({
      next: data => {
        //  this.dtTrigger.unsubscribe();
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.getDeliveries()
        this.onCloseHandled();
        this.deliveryForm.reset();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }

  openEditModal(delivery: any, i: any) {
    this.display1 = 'block';
    this.index = i;
    this.delivery.id = delivery.id
    this.deliveryForm.patchValue(
      {
        name: delivery.name,
        phone: delivery.phone
      }
    )
  }

  onUpdate() {
    this._deliveryService.UpdateDelivery(this.delivery.id, this.delivery).subscribe({
      next: data => {
        // trigger a re-render of the table
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        }); this.getDeliveries()
        this.onCloseEditHandled();
      },
      error: (error: any) => this.errorMessage = error,
    });
  }
  openModal() {
    this.display = 'block';
    this.deliveryForm.reset();
  }


  onCloseHandled() {
    this.display = 'none';
  }

  onCloseEditHandled() {
    this.display1 = 'none';
  }


}
