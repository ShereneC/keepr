import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log(res.data)
    AppState.vaults = res.data
  }
}

export const vaultsService = new VaultsService()
