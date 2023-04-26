import { Component, Input } from '@angular/core';

@Component({
  selector: 'travelpics-loader',
  templateUrl: './travelpics-loader.component.html',
  styleUrls: ['./travelpics-loader.component.scss']
})
export class TravelpicsLoaderComponent {
  @Input() isLoading = true;
  @Input() loadingMessage = '';
  constructor(){}
}
