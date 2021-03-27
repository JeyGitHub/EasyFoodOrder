import { Component, Input } from "@angular/core";
import { Restaurant } from "../models/restaurant.model";

@Component({
    selector: "restaurant-item",
    templateUrl: "./restaurant-item.component.html",
    styleUrls: ["./restaurant-item.component.css"]
})
export class RestaurantItemComponent {
    @Input() public restaurant: Restaurant;
}