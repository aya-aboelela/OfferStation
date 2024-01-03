import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnersIndexComponent } from 'src/app/pages/owners-index/owners-index.component';

const routes: Routes = [
  {
    path: ':category',
    component: OwnersIndexComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnersRoutingModule { }
