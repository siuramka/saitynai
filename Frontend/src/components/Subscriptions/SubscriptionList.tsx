import { useContext, useEffect, useState } from "react";
import { ISubscription } from "../../interfaces/Subscription/ISubscription";
import {
  deleteSubscription,
  getAllSubscriptions,
} from "../../services/subscription";
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Chip,
  ButtonGroup,
  Button,
} from "@mui/material";
import EditSubscriptionModal from "./EditSubscriptionModal";
import { NotificationContext } from "../../utils/context/NotificationContext";

const SubscriptionList = () => {
  const [subscriptions, setSubscriptions] = useState<ISubscription[]>();
  const [refresh, setRefresh] = useState(false);
  const { success } = useContext(NotificationContext);

  const handleRefresh = () => {
    setRefresh(!refresh);
  };

  const handleDelete = (
    shopId: number,
    softwareId: number,
    subscriptionId: number
  ) => {
    deleteSubscription({
      subscriptionId: subscriptionId,
      softwareId: softwareId,
      shopId: shopId,
    }).then(() => {
      success("Subscription deleted successfully!");
      handleRefresh();
    });
  };

  useEffect(() => {
    getAllSubscriptions().then((subscriptionsData) => {
      setSubscriptions(subscriptionsData);
    });
  }, [refresh]);

  return (
    <>
      <h2>Subscriptions</h2>
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
                    <ButtonGroup size="small" aria-label="small button group">
                      <Button
                        onClick={() =>
                          handleDelete(
                            subscription.software.shop.id,
                            subscription.software.id,
                            subscription.id
                          )
                        }
                      >
                        X
                      </Button>
                      <EditSubscriptionModal
                        handleRefresh={handleRefresh}
                        subscription={subscription}
                      />
                    </ButtonGroup>
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
};

export default SubscriptionList;
