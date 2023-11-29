import { useEffect, useState } from "react";
import { ISubscription } from "../../../interfaces/Subscription/ISubscription";
import { getAllSubscriptions } from "../../../services/subscription";
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Chip,
} from "@mui/material";
import EditSubscriptionModal from "./EditSubscriptionModal";

const SubscriptionList = () => {
  const [subscriptions, setSubscriptions] = useState<ISubscription[]>();
  const [refresh, setRefresh] = useState(false);

  const handleRefresh = () => {
    setRefresh(!refresh);
  };

  useEffect(() => {
    getAllSubscriptions().then((subscriptionsData) => {
      setSubscriptions(subscriptionsData);
    });
  }, [refresh]);

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell>Term</TableCell>
            <TableCell>Start date</TableCell>
            <TableCell>End date</TableCell>
            <TableCell>Total Price</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Software</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {subscriptions &&
            subscriptions.length > 0 &&
            subscriptions.map((subscription) => (
              <TableRow
                key={subscription.id}
                // sx={{
                //   cursor: "pointer",
                //   "&:hover": {
                //     backgroundColor: (theme) => theme.palette.action.hover,
                //   },
                // }}
                // onClick={() =>
                //   navigate(
                //     `/dashboard/shops/${subscription.software.shop.id}/softwares/${subscription.software.id}`
                //   )
                // }
              >
                <TableCell>{subscription.id}</TableCell>
                <TableCell>{subscription.termInMonths} mo</TableCell>
                <TableCell>{subscription.start.toString()}</TableCell>
                <TableCell>{subscription.end.toString()}</TableCell>
                <TableCell>{subscription.totalPrice}$</TableCell>
                <TableCell>
                  {subscription.isCanceled ? (
                    <Chip color="error" label="Canceled" />
                  ) : (
                    <Chip color="success" label="Active" />
                  )}
                </TableCell>
                <TableCell>{subscription.software.name}</TableCell>
                <TableCell>
                  <EditSubscriptionModal
                    handleRefresh={handleRefresh}
                    subscription={subscription}
                  />
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default SubscriptionList;
