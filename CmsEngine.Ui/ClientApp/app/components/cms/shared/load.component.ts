﻿import { Component, Input } from '@angular/core';

@Component({
  selector: 'cms-load',
  templateUrl: './load.component.html'
})
export class LoadComponent {
  @Input() loading = true;

  get isLoaded() {
    return this.loading;
  }
}