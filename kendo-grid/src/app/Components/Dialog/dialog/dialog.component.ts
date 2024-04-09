import { Component, EventEmitter, Input, Output} from '@angular/core';
import {  GridModule } from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [
    HttpClientModule,
    HttpClientJsonpModule,
    ReactiveFormsModule,
    GridModule,
    DialogModule,
    InputsModule,
    LabelModule,
    ButtonsModule,
  ],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.css'
})
export class DialogComponent {
  public active = false;
  public editForm: FormGroup = new FormGroup({
    ID: new FormControl(),
    FirstName: new FormControl("", [Validators.required]),
    LastName: new FormControl("", [Validators.required]),
    Email: new FormControl("", [Validators.required, Validators.email]),
    PhoneNumber: new FormControl("", [Validators.required]),
  });

  @Input() public isNew = false;
  @Input() public set model(client: any) {
    this.editForm.reset(client);
    this.active = client !== undefined;
  }

  @Output() cancel: EventEmitter<undefined> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  public onSave(e: PointerEvent): void {
    e.preventDefault();
    this.save.emit(this.editForm.value);
    this.closeForm();
  }

  public onCancel(e: PointerEvent): void {
    e.preventDefault();
    this.closeForm();
  }

  public closeForm(): void {
    this.active = false;
    this.cancel.emit();
  }
}
