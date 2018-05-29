import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";

@Component({
    selector: "dutch-cart",
    templateUrl: "cart.component.html",
    styleUrls: []
})
export class CartComponent {
    constructor(private data: DataService) {}
}