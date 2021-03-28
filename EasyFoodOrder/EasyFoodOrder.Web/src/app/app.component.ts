import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { OrderItem } from './models/order-item-model';
import { Restaurant } from './models/restaurant.model';
import { RestaurantService } from './services/restaurant.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public restaurants: Restaurant[];
  public searchInput: string;
  public dish: string;
  public city: string;
  public isLoading: boolean = false;
  public totalOrderCost: number = 0;
  public orderButtonCaption = "Order"
  public orderItems: OrderItem[] = [];
  private restaurantsReceivedSub: Subscription;

  constructor(private restaurantService: RestaurantService) { }

  public async ngOnInit(): Promise<void> {
    this.restaurantsReceivedSub = this.restaurantService.getRestaurantsReceivedObservable()
      .subscribe((data: { restaurants: Restaurant[] }) => {
        this.isLoading = true;
        this.restaurants = data.restaurants;
        this.isLoading = false;
      });
    this.isLoading = true;
    this.restaurantService.getRestaurants();
  }

  public onSearch(): void {
    this.isLoading = true;

    // TODO
    this.searchInput = "Taco In Cape Town";

    let isFilteredSearch: boolean = false;
    if (this.searchInput) {
      const separator = " in ";
      const separatorIndexLower = this.searchInput.toLowerCase();
      const separatorIndex = separatorIndexLower.indexOf(separator);
      const searchParts = separatorIndexLower.split(separator);
      if (searchParts.length === 2) {
        this.dish = this.searchInput.substr(0, separatorIndex);
        this.city = this.searchInput.substr(separatorIndex + separator.length);
        if (this.dish && this.city) {
          isFilteredSearch = true;
          this.restaurantService.getRestaurants(this.dish, this.city);
        }
      }
    }

    if (!isFilteredSearch) {
      this.restaurantService.getRestaurants();
    }
  }

  public addOrderItem(eventData: { orderItem: OrderItem, isAdded: boolean }): void {
    this.updateCost(eventData.orderItem, eventData.isAdded);
    this.updateOrderBtnCaption();
    this.updateOrderItems(eventData.orderItem, eventData.isAdded);
  }

  private updateCost(orderItem: OrderItem, isAdded: boolean): void {
    if (isAdded) {
      this.totalOrderCost += orderItem.price;
    } else {
      this.totalOrderCost -= orderItem.price;
    }
  }

  private updateOrderBtnCaption(): void {
    if (this.totalOrderCost > 0) {
      this.orderButtonCaption = `Order - R${Math.round((this.totalOrderCost + Number.EPSILON) * 100) / 100}`;
    } else {
      this.orderButtonCaption = "Order";
    }
  }

  private updateOrderItems(orderItem: OrderItem, isAdded: boolean): void {
    if (isAdded) {
      this.orderItems.push(orderItem);
    } else {
      this.orderItems = [...this.orderItems.filter(i => !(i.restaurantId === orderItem.restaurantId && i.menuItemId === orderItem.menuItemId))];
    }
  }

  public ngOnDestroy(): void {
    if (this.restaurantsReceivedSub) {
      this.restaurantsReceivedSub.unsubscribe();
    }
  }
}