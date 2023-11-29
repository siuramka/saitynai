import { ICartItem } from "./../interfaces/ICartItem";

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
