import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

import { environment } from "src/environments/environment";
import { Restaurant } from "../models/restaurant.model";

@Injectable()
export class RestaurantService {
    private restaurantsReceivedSub = new Subject<{restaurants: Restaurant[]}>();

    constructor(private httpService: HttpClient) {}

    public getRestaurantsReceivedObservable(): Observable<{ restaurants: Restaurant[] }> {
        return this.restaurantsReceivedSub.asObservable();
    }
    

    public async getRestaurantData(): Promise<Restaurant[]> {
        return this.httpService.get<Restaurant[]>(`${environment.apiUrl}/api/restaurants`).toPromise();
    }

    public getRestaurants(): void {
        this.httpService.get(`${environment.apiUrl}/api/restaurants`)
            .subscribe((restaurants: Restaurant[]) => {
                this.restaurantsReceivedSub.next({ restaurants });
            });
    }
}