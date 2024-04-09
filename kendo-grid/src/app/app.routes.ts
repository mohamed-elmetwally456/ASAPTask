import { Routes } from '@angular/router';
import { ClientsComponent } from './Components/Clients/clients/clients.component';
import { NotfoundComponent } from './Components/Not Found/notfound/notfound.component';
import { DialogComponent } from './Components/Dialog/dialog/dialog.component';

export const routes: Routes = [
  {path:"" , component:ClientsComponent,title:"home" ,pathMatch:"full"},
  {path:"clients",component:ClientsComponent,title:"clients"},
  {path:"dialog",component:DialogComponent,title:"dialog"},
  {path:'**',component:NotfoundComponent,title:"NotFound"},
];
