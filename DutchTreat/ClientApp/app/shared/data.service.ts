import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import "rxjs/add/operator/map";
import { Product } from "./products";
import { Order, OrderItem } from "./order";
import { tokenKey } from "@angular/core/src/view/util";

@Injectable()
export class DataService {
    private token: string = "";
    private tokenExpiration: Date;

    public products: Product[] = [];
    public order: Order = new Order();

    public get loginReuired(): boolean {
        return this.token.length === 0 || this.tokenExpiration > new Date();
    }

    constructor(private http: HttpClient) { }

    login(creds): Observable<boolean> {
        return this.http
            .post("/account/createtoken", creds)
            .map((data: any) => {
                this.token = data.token;
                this.tokenExpiration = data.expiration;

                return true;
            });
    }

    checkout(): Observable<boolean> {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }

        return this.http.post("/api/orders", this.order, {
                headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
            })
            .map(response => {
                this.order = new Order();

                return true;
            });
    }

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .map((data: any[]) => {
                this.products = data;

                return true;
            });
    }

    addToOrder(product: Product): void {
        let item: OrderItem = this.order.items.find(i => i.productId === product.id);

        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();

            item.productId = product.id;
            item.productArtist = product.artist;
            item.productArtId = product.artId;
            item.productCategory = product.category;
            item.productSize = product.size;
            item.productTitle = product.title;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);
        }        
    }
}