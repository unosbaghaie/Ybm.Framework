﻿import { Component } from 'angular2/core';
import { HTTP_PROVIDERS } from 'angular2/http';
import 'rxjs/Rx';   // Load all features

import { ProductListComponent } from './products/product-list.component';
import { ProductService } from './products/product.service';

@Component({
    selector: 'pm-app',
    template: `
    <div><h1>{{pageTitle}}</h1>
        <pm-products></pm-products>
        <div>My First Component</div>
    </div>
    `,
    directives: [ProductListComponent],
    providers:
    [
         ProductService
        , HTTP_PROVIDERS


    ]
})
export class AppComponent {
    pageTitle: string = "angular 2 sample";
}