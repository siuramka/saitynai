import { IShop } from "../Shop/IShop";
import { ISoftware } from "./ISoftware";
export interface ISoftwareWithShop extends ISoftware {
  shop: IShop;
}
