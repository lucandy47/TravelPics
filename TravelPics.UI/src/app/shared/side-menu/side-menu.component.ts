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
        onClick: () => console.log('home'),
        children: [],
      },
      {
        label: 'Posts',
        routerLink: null,
        listOrder: 2,
        icon:'pi pi-images',
        onClick: () => console.log('posts'),
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
        listOrder: 3,
        routerLink: 'profile',
        icon:'pi pi-fw pi-user',
        onClick: () => console.log('profile'),
        children: [],
      },
    ];
  }
}

