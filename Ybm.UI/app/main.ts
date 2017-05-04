/// <reference path="../node_modules/angular2/typings/browser.d.ts" />

import { bootstrap } from "angular2/platform/browser";
import {enableProdMode} from 'angular2/core';

// Our main component
import { AppComponent } from "./app.component";
enableProdMode();
bootstrap(AppComponent);