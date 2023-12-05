import { jwtDecode } from "jwt-decode";
import { ICartItem } from "./../interfaces/ICartItem";
import { User } from "./AuthSlice";
import { JwtPayload } from "../interfaces/JwtPayload";

export const isCartItemEqual = (
  item1: ICartItem,
  item2: ICartItem
): boolean => {
  return (
    item1.software.id === item2.software.id &&
    item1.subscription.TermInMonths === item2.subscription.TermInMonths &&
    item1.shopId === item2.shopId
  );
};

export const isItemInCart = (
  cartItems: ICartItem[],
  newItem: ICartItem
): boolean => {
  return cartItems.some((item) => {
    return (
      item.software.id === newItem.software.id &&
      item.subscription.TermInMonths === newItem.subscription.TermInMonths &&
      item.shopId === newItem.shopId
    );
  });
};

export const getUserFromTokens = (token: string, refreshToken: string) => {
  const decoded = jwtDecode<JwtPayload>(token);
  const user: User = {
    id: decoded.userId,
    role: decoded[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ],
    email:
      decoded[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ],
    exp: decoded.exp,
  };
  return user;
};

export const removeUserFromLocalStorage = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("refreshToken");
};

export const saveTokensToLocalStorage = (
  accessToken: string,
  refreshToken: string
) => {
  localStorage.setItem("token", accessToken);
  localStorage.setItem("refreshToken", refreshToken);
};

export const getUserFromLocalStorage = () => {
  const token = localStorage.getItem("token");
  const refreshToken = localStorage.getItem("refreshToken");
  if (!token || !refreshToken) return null;
  return getUserFromTokens(token, refreshToken);
};
