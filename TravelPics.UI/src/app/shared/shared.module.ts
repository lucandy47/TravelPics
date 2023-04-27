import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedRoutingModule } from './shared-routing.module';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { MenubarModule } from 'primeng/menubar';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { AvatarModule } from 'primeng/avatar';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ListboxModule } from 'primeng/listbox';
import { DividerModule } from 'primeng/divider';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { DialogModule } from 'primeng/dialog';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextareaModule } from 'primeng/inputtextarea';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ProgressBarModule } from 'primeng/progressbar';
import {SidebarModule} from 'primeng/sidebar';
import { CheckboxModule } from 'primeng/checkbox';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { SkeletonModule } from 'primeng/skeleton';
import { MultiSelectModule } from 'primeng/multiselect';
import { TooltipModule } from 'primeng/tooltip';
import { PaginatorModule } from 'primeng/paginator';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TreeModule } from 'primeng/tree';
import { PasswordModule } from 'primeng/password';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FileUploadModule } from 'primeng/fileupload';
import { AgmCoreModule } from '@agm/core';
import { GalleriaModule } from 'primeng/galleria';
import { TravelpicsLoaderComponent } from './travelpics-loader/travelpics-loader.component';
import { CarouselModule } from 'primeng/carousel';

const PrimeModules = [
  TableModule,
  DropdownModule,
  CalendarModule,
  ButtonModule,
  TagModule,
  MenubarModule,
  ButtonModule,
  TieredMenuModule,
  AvatarModule,
  InputTextModule,
  RadioButtonModule,
  ListboxModule,
  DividerModule,
  CascadeSelectModule,
  FormsModule,
  ReactiveFormsModule,
  CheckboxModule,
  ScrollPanelModule,
  ProgressSpinnerModule,
  SkeletonModule,
  MultiSelectModule,
  TooltipModule,
  PaginatorModule,
  InputNumberModule,
  PasswordModule,
  MessageModule,
  MessagesModule,
  DialogModule,
  AutoCompleteModule,
  InputTextareaModule,
  OverlayPanelModule,
  DynamicDialogModule,
  ConfirmDialogModule,
  ToastModule,
  TreeModule,
  ProgressBarModule,
  SidebarModule,
  FileUploadModule,
  GalleriaModule,
  CarouselModule
]

@NgModule({
  declarations: [
    TravelpicsLoaderComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    FontAwesomeModule,
    ...PrimeModules,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCohK-ELARdxi7pNbEwAFTEy8cKUEzEwfE',
      libraries: ['places']
    })
  ],
  exports: [
    CommonModule,
    FontAwesomeModule,
    ...PrimeModules,
    TravelpicsLoaderComponent
  ],
    providers: [
    ConfirmationService,
  ],
})
export class SharedModule { }
