import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { ProductListComponent } from "./shop/product-list.component";
import { DataService } from "./shared/data.service";
import { CartComponent } from "./shop/card.component";

@NgModule({
    declarations: [
        AppComponent,
        ProductListComponent,
        CartComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
