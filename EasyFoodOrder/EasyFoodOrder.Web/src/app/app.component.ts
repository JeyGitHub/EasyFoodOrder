import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public title: string = 'EasyFoodOrderWeb';
  public dataLength: number;

  constructor(private http: HttpClient) {}

  public ngOnInit(): void {
    
  }
  
  public loadData(): void {
    this.http.get("http://localhost:5000/api/restaurants").subscribe((data: any) => {
      this.dataLength = data.length;
      console.log(data);
    });
  }
}