import {
  Button,
  Modal,
  Box,
  Typography,
  Stack,
  TextField,
  FormControlLabel,
  Switch,
} from "@mui/material";
import { useState } from "react";
import { ISubscription } from "../../../interfaces/Subscription/ISubscription";
import { editSubscription } from "../../../services/subscription";

type EditSubscriptionModalParams = {
  handleRefresh: () => void;
  subscription: ISubscription;
};

const EditSubscriptionModal = ({
  handleRefresh,
  subscription,
}: EditSubscriptionModalParams) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [formData, setFormData] = useState({
    TermInMonths: subscription.termInMonths,
    IsCanceled: subscription.isCanceled,
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleCanceledSwitchChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const isChecked = event.target.checked;
    setFormData({ ...formData, IsCanceled: isChecked });
  };

  const style = {
    position: "absolute" as "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: "50vh",
    bgcolor: "background.paper",
    p: 4,
    pb: 10,
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const editSubscriptionData: EditSubscription = {
      ...formData,
      Id: subscription.id,
    };

    const editedSubscription = await editSubscription({
      subscription: editSubscriptionData,
      shopId: subscription.software.shop.id,
      softwareId: subscription.software.id,
    });
    if (editedSubscription) {
      handleRefresh();
      handleClose();
    }
  };

  return (
    <>
      <Button onClick={handleOpen}>Edit</Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box component="form" sx={style} onSubmit={handleSubmit}>
          <Box
            sx={{
              pb: 6,
              pt: 2,
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Edit subscription
            </Typography>
          </Box>
          <Box>
            <Stack spacing={2}>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  disabled
                  label="Software name"
                  value={subscription.software.name}
                  onChange={handleInputChange}
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  name="TermInMonths"
                  type="number"
                  inputProps={{ min: 1, max: 24 }}
                  label="Add months to subscription"
                  value={formData.TermInMonths}
                  onChange={handleInputChange}
                  onKeyDown={(e) => {
                    e.preventDefault();
                  }}
                  required
                />
              </div>
              <div>
                <FormControlLabel
                  control={
                    <Switch
                      value={formData.IsCanceled}
                      name="IsCanceled"
                      onChange={handleCanceledSwitchChange}
                    />
                  }
                  label="Cancel"
                />
              </div>
              <div>
                <Box
                  sx={{
                    pt: 4,
                    display: "flex",
                    justifyContent: "space-between",
                  }}
                >
                  <Button variant="contained" type="submit">
                    Edit
                  </Button>
                </Box>
              </div>
            </Stack>
          </Box>
        </Box>
      </Modal>
    </>
  );
};
export default EditSubscriptionModal;
