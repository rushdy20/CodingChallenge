import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { County } from '../models/county';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:44341/health', { responseType: 'text' });
  }
  public getAllCountries(){
    return this.httpClient.get<County[]>('https://localhost:44303/api/PaymentSense');
  }
}
