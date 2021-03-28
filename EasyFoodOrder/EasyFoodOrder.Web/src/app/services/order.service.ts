import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

import { environment } from "src/environments/environment";
import { OrderItem } from "../models/order-item-model";

@Injectable()
export class OrderService {
    private orderCreatedSub = new Subject<number>();

    constructor(private httpService: HttpClient) {}

    public getOrderCreatedObservable(): Observable<number> {
        return this.orderCreatedSub.asObservable();
    }

    public createOrder(orderItems: OrderItem[]): void {
        let apiUrl: string = `${environment.apiUrl}/api/orders`;
        this.httpService.post(apiUrl, orderItems)
            .subscribe((orderId: number) => {
                this.orderCreatedSub.next(orderId);
            });
    }
}