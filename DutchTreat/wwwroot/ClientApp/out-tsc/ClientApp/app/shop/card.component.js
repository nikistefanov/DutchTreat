"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var data_service_1 = require("../shared/data.service");
var router_1 = require("@angular/router");
var CartComponent = /** @class */ (function () {
    function CartComponent(data, router) {
        this.data = data;
        this.router = router;
    }
    CartComponent.prototype.onCheckout = function () {
        if (this.data.loginReuired) {
            this.router.navigate(["login"]);
        }
        else {
            this.router.navigate(["checkout"]);
        }
    };
    CartComponent = __decorate([
        core_1.Component({
            selector: "dutch-cart",
            templateUrl: "cart.component.html",
            styleUrls: []
        }),
        __metadata("design:paramtypes", [data_service_1.DataService, router_1.Router])
    ], CartComponent);
    return CartComponent;
}());
exports.CartComponent = CartComponent;
//# sourceMappingURL=card.component.js.map