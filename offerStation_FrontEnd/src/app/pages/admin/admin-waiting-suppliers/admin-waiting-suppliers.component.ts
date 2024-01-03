import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { AdminApprovalWaitingService } from 'src/app/services/admin/waiting/admin-approval-waiting.service';
import { ImageService } from 'src/app/services/image.service';
import { TraderDetails } from 'src/app/sharedClassesAndTypes/TraderDetails';

@Component({
  selector: 'app-admin-waiting-suppliers',
  templateUrl: './admin-waiting-suppliers.component.html',
  styleUrls: ['./admin-waiting-suppliers.component.css']
})
export class AdminWaitingSuppliersComponent {

  Suppliers: TraderDetails[] = [];

  dtOptions:DataTables.Settings = {};
  dtTrigger:Subject<any> = new Subject<any>(); 

  constructor(
    private _imageService:ImageService,
    private _waitingSuppliersService:AdminApprovalWaitingService
    ) {}

  ngOnInit(): void {

    this.dtOptions={
      pagingType:'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true,
      destroy:true
    }
    this.getWaitingSuppliers();
  }

  getWaitingSuppliers(): void {
    this._waitingSuppliersService.GetAllWaitingSuppliers()
      .subscribe(response => 
        {
          this.Suppliers = response.data
          this.dtTrigger.next(null);
          this.Suppliers.forEach((supplier:TraderDetails)=>{
            supplier.image =this._imageService.base64ArrayToImage(supplier.image)          
            });
        });
  }

  onDelete(index:number, ownerId:number){
    this._waitingSuppliersService.DeleteSupplier(ownerId)
    .subscribe({
      next: data => {
        this.dtTrigger.unsubscribe();
        this.Suppliers.splice(index, 1);
        this.getWaitingSuppliers();
      }
    });
  }
  onApprove(index:number, ownerId:number){
    this._waitingSuppliersService.ApproveSupplier(ownerId)
    .subscribe({
      next: data => {
        this.dtTrigger.unsubscribe();
        this.Suppliers.splice(index, 1);
        this.getWaitingSuppliers();
      }
    });
  }
}
