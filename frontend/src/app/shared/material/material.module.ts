import { DragDropModule } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import {MatCheckbox, MatCheckboxModule} from "@angular/material/checkbox";

export { MatSelectModule } from '@angular/material/select';

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        MatButtonModule,
        MatSlideToggleModule,
        MatListModule,
        MatSelectModule,
        MatMenuModule,
        MatSnackBarModule,
        MatDividerModule,
        MatInputModule,
        MatRadioModule,
        MatFormFieldModule,
        MatIconModule,
        MatTableModule,
        MatTabsModule,
        MatSlideToggleModule,
        MatDatepickerModule,
        MatNativeDateModule,
        DragDropModule,
        MatAutocompleteModule,
        MatCheckboxModule,
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        MatCardModule,
        MatButtonModule,
        MatSlideToggleModule,
        MatListModule,
        MatSelectModule,
        MatMenuModule,
        MatSnackBarModule,
        MatDividerModule,
        MatInputModule,
        MatRadioModule,
        MatFormFieldModule,
        MatIconModule,
        MatTableModule,
        MatTabsModule,
        MatSlideToggleModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatAutocompleteModule,
        DragDropModule,
        MatCheckboxModule,
    ],
})
export class MaterialModule {}
