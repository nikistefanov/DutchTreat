import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import "rxjs/add/operator/map";
import { Product } from "./products";
import { Order, OrderItem } from "./order";

@Injectable()
export class DataService {
    public products: Product[] = [];
    public order: Order = new Order();

    constructor(private http: HttpClient) { }

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