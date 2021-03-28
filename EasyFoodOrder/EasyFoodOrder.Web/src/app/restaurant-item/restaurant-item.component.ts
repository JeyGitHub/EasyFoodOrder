import { Component, EventEmitter, Input, Output } from "@angular/core";
import { MatCheckboxChange } from "@angular/material/checkbox";
import { OrderItem } from "../models/order-item-model";
import { Restaurant } from "../models/restaurant.model";

@Component({
    selector: "restaurant-item",
    templateUrl: "./restaurant-item.component.html",
    styleUrls: ["./restaurant-item.component.css"]
})
export class RestaurantItemComponent {
    @Input() public restaurant: Restaurant;
    @Output() public orderItemChanged = new EventEmitter<{ orderItem: OrderItem, isAdded: boolean }>();
    public selectedItems: number[];

    public onCheckboxStateChanged(eventData: MatCheckboxChange): void {
        const restaurantId = eventData.source._elementRef.nativeElement.restaurantId;
        const menuItemId = eventData.source._elementRef.nativeElement.menuItemId;
        const price = eventData.source._elementRef.nativeElement.price;
        const orderItem: OrderItem = new OrderItem(restaurantId, menuItemId, price);
        this.orderItemChanged.next({ orderItem, isAdded: eventData.checked });
    }
}