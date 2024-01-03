import { Component, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { AdminApprovalWaitingService } from 'src/app/services/admin/waiting/admin-approval-waiting.service';
import { ImageService } from 'src/app/services/image.service';
import { TraderDetails } from 'src/app/sharedClassesAndTypes/TraderDetails';
import { DataTableDirective } from 'angular-datatables';

@Component({
  selector: 'app-admin-waiting-owners',
  templateUrl: './admin-waiting-owners.component.html',
  styleUrls: ['./admin-waiting-owners.component.css']
})
export class AdminWaitingOwnersComponent {
 
  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;
  
  Owners: TraderDetails[] = [];

  dtOptions:DataTables.Settings = {};
  dtTrigger:Subject<any> = new Subject<any>(); 

  constructor(
    private _imageService:ImageService,
    private _waitingOwnersService:AdminApprovalWaitingService
    ) {}

  ngOnInit(): void {

    this.dtOptions={
      pagingType:'full_numbers',
      pageLength: 5,
      lengthMenu : [5, 10, 20],
      processing: true,
      destroy:true,
      responsive: true,
      language: {
        search: '_INPUT_',
        searchPlaceholder: 'Search records',
      }
    }
    this.getWaitingOwners();
  }

  getWaitingOwners(): void {
    this._waitingOwnersService.GetAllWaitingOwners()
      .subscribe(response => 
        {
          this.Owners = response.data
          this.dtTrigger.next(null);
          this.Owners.forEach((owner:TraderDetails)=>{
            owner.image =this._imageService.base64ArrayToImage(owner.image)          
            });
        });
  }

  onDelete(index:number, ownerId:number){
    this._waitingOwnersService.DeleteOwner(ownerId)
    .subscribe({
      next: data => {
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        // this.dtTrigger.unsubscribe();
        this.Owners.splice(index, 1);
        this.getWaitingOwners();
      }
    });
  }
  onApprove(index:number, ownerId:number){
    this._waitingOwnersService.ApproveOwner(ownerId)
    .subscribe({
      next: data => {
        // this.dtTrigger.unsubscribe();
        this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next(null);
        });
        this.Owners.splice(index, 1);
        this.getWaitingOwners();
      }
    });
  }
}
