import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log(res.data)
    AppState.vaults = res.data
  }

  async createVault(vault) {
    const res = await api.post('/api/vaults', vault)
    logger.log(res.data)
    AppState.vaults.push(res.data)
  }
}

export const vaultsService = new VaultsService()
