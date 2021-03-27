import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Restaurant } from './models/restaurant.model';
import { RestaurantService } from './services/restaurant.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public r: Restaurant;
  public restaurants: Restaurant[];
  public isLoading: boolean = false;
  private restaurantsReceivedSub: Subscription;

  constructor(private restaurantService: RestaurantService) { }

  public async ngOnInit(): Promise<void> {
    this.restaurantsReceivedSub = this.restaurantService.getRestaurantsReceivedObservable()
      .subscribe((data: { restaurants: Restaurant[] }) => {
        this.isLoading = true;
        this.restaurants = data.restaurants;
        this.r = this.restaurants[0];
        this.isLoading = false;
      });
    this.isLoading = true;
    this.restaurantService.getRestaurants();
  }

  public onSearch(): void {
    console.log('search');
  }

  public ngOnDestroy(): void {
    if (this.restaurantsReceivedSub) {
      this.restaurantsReceivedSub.unsubscribe();
    }
  }
}