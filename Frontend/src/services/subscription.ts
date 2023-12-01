import { CreateSubscription } from "../interfaces/Subscription/CreateSubscription";
import { ISubscription } from "../interfaces/Subscription/ISubscription";
import { ISubscriptionCreated } from "../interfaces/Subscription/ISubscriptionCreated";
import api from "./api";

type EditSubscriptionProps = {
  subscription: EditSubscription;
  shopId: number;
  softwareId: number;
};

export const editSubscription = async ({
  subscription,
  shopId,
  softwareId,
}: EditSubscriptionProps) => {
  try {
    const response = await api.put<EditSubscription>(
      `shops/${shopId}/softwares/${softwareId}/subscriptions/${subscription.Id}`,
      subscription
    );
    if (response.status === 200) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

export const getAllSubscriptions = async () => {
  try {
    const response = await api.get<ISubscription[]>(`subscriptions`);

    if (response.status === 200) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

type CreateSubscriptionProps = {
  subscription: CreateSubscription;
  shopId: number;
  softwareId: number;
};

export const createSubscription = async ({
  subscription,
  shopId,
  softwareId,
}: CreateSubscriptionProps) => {
  try {
    const response = await api.post<ISubscriptionCreated>(
      `shops/${shopId}/softwares/${softwareId}/subscriptions`,
      subscription
    );

    if (response.status === 201) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

type DeleteSubscriptionProps = {
  subscriptionId: number;
  shopId: number;
  softwareId: number;
};

export const deleteSubscription = async ({
  subscriptionId,
  shopId,
  softwareId,
}: DeleteSubscriptionProps) => {
  try {
    const response = await api.delete<ISubscriptionCreated>(
      `shops/${shopId}/softwares/${softwareId}/subscriptions/${subscriptionId}`
    );

    if (response.status === 204) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};
