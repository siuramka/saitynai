import {
  Button,
  Modal,
  Box,
  Typography,
  Chip,
  Stack,
  TextField,
} from "@mui/material";
import { useState } from "react";
import { createShopSoftware } from "../../services/software";
import { CreateSoftware } from "../../interfaces/Software/CreateSoftware";

type CreateSoftwareModalParams = {
  handleRefresh: () => void;
  shopId: number;
};
const CreateSoftwareModal = ({
  handleRefresh,
  shopId,
}: CreateSoftwareModalParams) => {
  const [open, setOpen] = useState(false);

  const [formData, setFormData] = useState({
    Name: "",
    Description: "",
    PriceMonthly: 0,
    Website: "",
    Instructions: "",
  });

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

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
    const software: CreateSoftware = formData;
    const createdSoftware = await createShopSoftware({
      software: software,
      shopId: shopId,
    });
    if (createdSoftware) {
      handleRefresh();
      handleClose();
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
              Create a new software for the shop.
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
                  name="Name"
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
                  name="Description"
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
                  label="Price monthly"
                  value={formData.PriceMonthly}
                  name="PriceMonthly"
                  type="number"
                  onChange={handleInputChange}
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Website"
                  name="Website"
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  multiline
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 256 }}
                  label="Installation instructions"
                  name="Instructions"
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
export default CreateSoftwareModal;
