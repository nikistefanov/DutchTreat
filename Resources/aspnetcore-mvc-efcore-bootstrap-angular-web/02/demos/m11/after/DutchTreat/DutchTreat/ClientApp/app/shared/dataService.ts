import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs"
import 'rxjs/add/operator/map';
import { Product } from "./product";

@Injectable()
export class DataService {

  constructor(private http: HttpClient) { }

  public products: Product[] = [];

  loadProducts(): Observable<boolean> {
    return this.http.get("/api/products")
      .map((data: any[]) => {
        this.products = data;
        return true;
      });
  }

}