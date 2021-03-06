import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaultById(id) {
    // debugger
    const found = AppState.vaults.find(v => v.id.toString() === id)
    if (found) {
      AppState.activeVault = found
      return
    }
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async createVault(vault) {
    const res = await api.post('/api/vaults', vault)
    AppState.vaults.push(res.data)
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
  }
}

export const vaultsService = new VaultsService()
