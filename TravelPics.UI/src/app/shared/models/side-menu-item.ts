import { Observable } from "rxjs";

export interface SideMenuItem{
    label: string;
    icon: string;
    onClick():any;
    listOrder:number;
    routerLink: string | null;
    children: SideMenuItem[];
}