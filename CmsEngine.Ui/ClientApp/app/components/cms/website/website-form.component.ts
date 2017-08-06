﻿import { Component, AfterViewInit, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastyService } from 'ng2-toasty';

import { WebsiteService } from '../../../services/website.service';
import { WebsiteEditModel } from '../../../models/website-editmodel';

@Component({
  selector: 'cms-website-form',
  templateUrl: './website-form.component.html',
  providers: [WebsiteService]
})
export class WebsiteFormComponent implements OnInit {
  public websiteEditModel: WebsiteEditModel = {
    id: 0,
    vanityId: '',
    name: '',
    description: '',
    culture: '',
    urlFormat: '',
    dateFormat: '',
    siteUrl: ''
  };

  constructor(private websiteService: WebsiteService, private route: ActivatedRoute, private toastyService: ToastyService) {
    route.params.subscribe(p => {
      this.websiteEditModel.vanityId = p["id"];
    });
  }

  public ngOnInit(): void {
    if (this.websiteEditModel.vanityId) {
      this.websiteService.get(this.websiteEditModel.vanityId)
        .subscribe(website => {
          this.websiteEditModel = website;
        });
    }
  }

  public onSubmit() {
    if (this.websiteEditModel.id || this.websiteEditModel.vanityId) {
      this.websiteService.update(this.websiteEditModel)
        .subscribe(response => {
          this.toastyService.success({
            title: 'Success',
            msg: response.message,
            theme: 'bootstrap',
            showClose: true,
            timeout: 10000
          });
        });
    } else {
      this.websiteService.create(this.websiteEditModel)
        .subscribe(response => {
          this.toastyService.success({
            title: 'Success',
            msg: response.message,
            theme: 'bootstrap',
            showClose: true,
            timeout: 10000
          });
        });
    }
  }
}
