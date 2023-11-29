export interface ISubscriptionCreated {
  Id: number;
  TermInMonths: number;
  Start: Date;
  End: Date;
  TotalPrice: number;
  IsCanceled: boolean;
}
