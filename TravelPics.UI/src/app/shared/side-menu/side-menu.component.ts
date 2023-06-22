import { Component, Input, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { SideMenuItem } from '../models/side-menu-item';
import { ActivatedMenuGroup } from '../models/activated-menu-group';

@Component({
  selector: 'travelpics-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent implements OnInit {
  @Input() baseRoute: string = '';
  constructor(
  ) {

  }
  
  menuItems: SideMenuItem[] = [];
  expanded: boolean = false;

  ngOnInit(): void {
    this.menuItems = [
      {
        label: 'Home',
        listOrder: 1,
        routerLink: 'home',
        icon:'pi pi-home',
        onClick: () => {},
        children: [],
      },
      {
        label: 'Popular',
        routerLink: null,
        listOrder: 2,
        icon:'pi pi-star',
        onClick: () => {},
        children: [
          {
            label: 'Most Appreciated',
            routerLink: 'posts/most-appreciated',
            listOrder: 1,
            icon:'pi pi-thumbs-up',
            onClick: () => {},
            children: [],
          },
        ],
      },
      {
        label: 'Posts',
        routerLink: null,
        listOrder: 3,
        icon:'pi pi-images',
        onClick: () => {},
        children: [
          {
            label: 'New Post',
            routerLink: 'posts/new',
            listOrder: 1,
            icon:'pi pi-images',
            onClick: () => {},
            children: [],
          },
          {
            label: 'My Posts',
            routerLink: 'posts/my',
            listOrder: 2,
            icon:'pi pi-images',
            onClick: () => {},
            children: [],
          },
        ],
      },
      {
        label: 'Profile',
        listOrder: 4,
        routerLink: 'profile',
        icon:'pi pi-fw pi-user',
        onClick: () => {},
        children: [],
      },
    ];
  }
}

