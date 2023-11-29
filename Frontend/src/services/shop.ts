import { IShop } from "../interfaces/Shop/IShop";
import { CreateShop } from "../interfaces/Shop/CreateShop";
import { EditShop } from "../interfaces/Shop/EditShop";
import api from "./api";

export const getShops = async () => {
  try {
    const response = await api.get<IShop[]>("shops");

    if (response.status === 200) {
      return response.data;
    }

    return undefined;
  } catch {
    return undefined;
  }
};

type EditShopParams = {
  shopId: number;
  shop: EditShop;
};

export const editShop = async ({ shopId, shop }: EditShopParams) => {
  try {
    const response = await api.put<IShop>(`shops/${shopId}`, shop);
    if (response.status === 200) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};

export type CreateShopParams = {
  shop: CreateShop;
};

export const createShop = async ({ shop }: CreateShopParams) => {
  try {
    const response = await api.post<IShop>("shops", shop);
    if (response.status === 201) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};
