import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';

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
        MatIconModule,
        MatTableModule,
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
        MatIconModule,
        MatTableModule,
    ],
})
export class MaterialModule {}
