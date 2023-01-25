import { Component, OnInit} from '@angular/core';
import { County } from './models/county';
import { PaymentsenseCodingChallengeApiService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  public countries: Array<County> = [];
  public country: County = new County();

  constructor(private apiService: PaymentsenseCodingChallengeApiService) { }
  ngOnInit(): void {

    this.apiService.getAllCountries().subscribe(res => this.countries = res);
  }

 public getCountry(capital: string) {
    
   this.country = this.countries.filter(c => c.capital == capital)[0];
  }


}
