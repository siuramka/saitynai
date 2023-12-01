import { IShop } from "../interfaces/Shop/IShop";
import { CreateShop } from "../interfaces/Shop/CreateShop";
import { EditShop } from "../interfaces/Shop/EditShop";
import api from "./api";
import { IPagination } from "../interfaces/IPagination";
import { IPagedData } from "../interfaces/IPagedData";

export const getShops = async (page?: number) => {
  try {
    const endpoint = page ? `shops?PageNumber=${page}` : "shops";
    const response = await api.get<IShop[]>(endpoint);

    if (response.status === 200) {
      const paginationHeader = response.headers["x-pagination"];

      if (paginationHeader) {
        const pagination: IPagination = JSON.parse(paginationHeader);
        const pagedData: IPagedData<IShop[]> = {
          data: response.data,
          pagination: pagination,
        };
        return pagedData;
      }
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

type DeleteShopParams = {
  shopId: number;
};

export const deleteShop = async ({ shopId }: DeleteShopParams) => {
  try {
    const response = await api.delete<IShop>(`shops/${shopId}`);
    if (response.status === 204) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};

type GetShopParams = {
  shopId: number;
};

export const getShop = async ({ shopId }: GetShopParams) => {
  try {
    const response = await api.get<IShop>(`shops/${shopId}`);
    if (response.status === 200) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};
