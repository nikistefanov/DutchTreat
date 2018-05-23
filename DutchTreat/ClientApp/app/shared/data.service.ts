import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import "rxjs/add/operator/map";
import { Product } from "./products";

@Injectable()
export class DataService {
    public products: Product[] = [];

    constructor(private http: HttpClient) { }

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .map((data: any[]) => {
                this.products = data;

                return true;
            });
    }
}