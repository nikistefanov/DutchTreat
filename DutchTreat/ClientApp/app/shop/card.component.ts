import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";
import { Router } from "@angular/router";

@Component({
    selector: "dutch-cart",
    templateUrl: "cart.component.html",
    styleUrls: []
})
export class CartComponent {
    constructor(private data: DataService, private router: Router) { }

    onCheckout() {
        if (this.data.loginReuired) {
            this.router.navigate(["login"]);
        } else {
            this.router.navigate(["checkout"]);
        }
    }
}