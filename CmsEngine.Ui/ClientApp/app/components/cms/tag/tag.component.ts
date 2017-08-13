﻿import { Component, AfterViewInit } from '@angular/core';
import { Router } from "@angular/router";
import { ToastyService } from 'ng2-toasty';

import { TagService } from '../../../services/tag.service';
import { ToastType } from '../../../models/index';

@Component({
  selector: 'cms-tag',
  templateUrl: './tag.component.html',
  providers: [TagService]
})
export class TagComponent implements AfterViewInit {
  public tags = [];
  public columns = [];
  public vanityId: string;

  constructor(
    private tagService: TagService,
    private toastyService: ToastyService
  ) { }

  public ngAfterViewInit(): void {
    this.loadData();
  }

  public onDeleteTag(vanityId: string) {
    this.tagService.delete(vanityId)
      .subscribe(response => {
        this.tagService.showToast(ToastType.Success, response.message);
        this.loadData();
      }, err => {
        this.tagService.showToast(ToastType.Error, err.message);
      });
  }

  private loadData() {
    this.tagService.get()
      .subscribe(tags => {
        this.tags = tags;
        this.columns = this.tagService.extractProperties(this.tags[0]);
      }, err => {
        this.tagService.showToast(ToastType.Error, err.message);
      });
  }
}