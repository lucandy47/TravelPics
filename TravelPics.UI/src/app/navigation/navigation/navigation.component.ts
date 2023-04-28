import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'travelpics-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit{

  public baseRoute: string = '';

  constructor(
    private activatedRoute: ActivatedRoute
  ){}
  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data)=>{
      this.baseRoute = data.baseRoute;
    })
  }

}
