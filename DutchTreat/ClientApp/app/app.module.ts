import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";

import { AppComponent } from "./app.component";
import { ProductListComponent } from "./shop/product-list.component";
import { DataService } from "./shared/data.service";
import { CartComponent } from "./shop/card.component";
import { ShopComponent } from "./shop/shop.component";
import { CheckoutComponent } from "./checkout/checkout.component";
import { LoginComponent } from "./login/login.component";

const routes = [
    { path: "", component: ShopComponent },
    { path: "checkout", component: CheckoutComponent },
    { path: "login", component: LoginComponent }
]

@NgModule({
    declarations: [
        AppComponent,
        ProductListComponent,
        CartComponent,
        ShopComponent,
        CheckoutComponent,
        LoginComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false // for Debugging
        })
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
