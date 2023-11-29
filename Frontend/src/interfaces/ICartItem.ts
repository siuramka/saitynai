import { ISoftware } from "./Software/ISoftware";
import { CreateSubscription } from "./Subscription/CreateSubscription";

export interface ICartItem {
  software: ISoftware;
  subscription: CreateSubscription;
  shopId: number;
}
