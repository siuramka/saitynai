import { ISoftwareWithShop } from "../Software/ISoftwareWithShop";

export interface ISubscription {
  id: number;
  termInMonths: number;
  start: Date;
  end: Date;
  totalPrice: number;
  isCanceled: boolean;
  software: ISoftwareWithShop;
}
