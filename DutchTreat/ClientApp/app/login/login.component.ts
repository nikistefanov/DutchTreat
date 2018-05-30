import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";
import { Router } from "@angular/router";

@Component({
    selector: "dutch-login",
    templateUrl: "login.component.html"
})
export class LoginComponent {
    public creds = {
        username: "",
        password: ""
    };
    public errorMessage: string;

    constructor(private data: DataService, private router: Router) { }

    onLogin() {
        this.data.login(this.creds)
            .subscribe(success => {
                if (success) {
                    if (this.data.order.items.length === 0) {
                        this.router.navigate(["/"]);
                    } else {
                        this.router.navigate(["checkout"]);
                    }
                }
            }, err => this.errorMessage = "Failed to login")
    }
}