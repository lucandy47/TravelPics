import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavigationRoutingModule } from './navigation-routing.module';
import { NavigationComponent } from './navigation/navigation.component';
import { SharedModule } from "../shared/shared.module";


@NgModule({
    declarations: [
        NavigationComponent
    ],
    imports: [
        CommonModule,
        NavigationRoutingModule,
        SharedModule
    ]
})
export class NavigationModule { }
