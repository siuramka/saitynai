import {
  Button,
  Modal,
  Box,
  Typography,
  Stack,
  TextField,
} from "@mui/material";
import { useContext, useState } from "react";
import { createShop } from "../../services/shop";
import { CreateShop } from "../../interfaces/Shop/CreateShop";
import { NotificationContext } from "../../utils/context/NotificationContext";

type CreateShopModalParams = {
  handleRefresh: () => void;
};

const CreateShopModal = ({ handleRefresh }: CreateShopModalParams) => {
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const { success } = useContext(NotificationContext);

  const [formData, setFormData] = useState({
    name: "",
    description: "",
    contactInformation: "",
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
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
    const newShop: CreateShop = formData;
    const createdShop = await createShop({ shop: newShop });
    if (createdShop) {
      handleRefresh();
      handleClose();
      success("Successfully created shop!");
    }
  };

  return (
    <>
      <Button onClick={handleOpen}>Create</Button>
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
              Create a new shop.
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
                  label="Name"
                  name="name"
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Description"
                  name="description"
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Contacts (email, discord, etc)"
                  name="contactInformation"
                  onChange={handleInputChange}
                  required
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
                    Create
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
export default CreateShopModal;
