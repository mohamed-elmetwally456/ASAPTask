import { Component, OnInit } from '@angular/core';
import { ClientsService } from '../../../Services/clients.service';
import { GridModule } from '@progress/kendo-angular-grid';
import { RouterLink } from '@angular/router';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { Observable} from 'rxjs';
import { State } from '@progress/kendo-data-query';
import { DialogComponent } from "../../Dialog/dialog/dialog.component";
@Component({
    selector: 'app-clients',
    standalone: true,
    templateUrl: './clients.component.html',
    styleUrl: './clients.component.css',
    imports: [RouterLink, HttpClientModule,
        HttpClientJsonpModule,
        ReactiveFormsModule,
        GridModule,
        DialogModule,
        InputsModule,
        LabelModule,
        ButtonsModule, DialogComponent]
})
export class ClientsComponent implements OnInit {
  public view!: Observable<any>;
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10,
  };
  public allClients: any[] = [];
  public editDataItem: any;
  public isNew = false;
  public currentClientId: number | null = null;
  constructor(private clientsService: ClientsService) { }

  ngOnInit(): void {
    this.getClients();
  }

  getClients(): void {
    this.view = this.clientsService.getAllClients();
    this.view.subscribe({
      next: (response) => {
        this.allClients = response;
        console.log(response);
      },
      error: (error) => {
        console.log('Error fetching clients:', error);
      }
    });
  }

  public onStateChange(state: State): void {
    this.gridState = state;
    this.getClients();
  }

  public addHandler(): void {
    this.editDataItem = { firstName: '', lastName: '', emailAddress: '', phoneNumber: '' };
    this.isNew = true;
  }

  public editHandler(args: any,ClientId:number): void {
    this.editDataItem = { ...args.dataItem };
    this.isNew = false;
    this.currentClientId = ClientId;
  }

  public cancelHandler(): void {
    this.editDataItem = undefined;
  }

  public saveHandler(client: any): void {
    if (this.isNew) {
      this.clientsService.addClient(client).subscribe({
        next: () => {
          this.getClients();
          this.editDataItem = undefined;
          this.isNew = false;
        },
        error: (error) => {
          console.log('Error adding client:', error);
        }
      });
    } else if (this.currentClientId !== null) {
      client.clientId = this.currentClientId;
      this.clientsService.updateClient(this.currentClientId, client).subscribe({
        next: () => {
          this.getClients();
          this.editDataItem = undefined;
        },
        error: (error) => {
          console.log('Error updating client:', error);
        }
      });
    } else {
      console.log('No client ID available for saving.');
    }
  }

  public removeHandler(clientId: number): void {
    this.clientsService.deleteClient(clientId).subscribe({
      next: () => {
        this.getClients();
      },
      error: (error) => {
        console.log('Error deleting client:', error);
      }
    });

  }
}
