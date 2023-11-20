import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'users-front';

  constructor(private primengConfig: PrimeNGConfig) {

  }

  ngOnInit() {
    this.primengConfig.ripple = true;
  }
}
