﻿using ECommerceEnterprise.Pedido.API.Application.DTO;
using ECommerceEnterprise.Pedidos.Domain.Vouchers;

namespace ECommerceEnterprise.Pedido.API.Application.Queries;

public interface IVoucherQueries
{
    Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
}
public class VoucherQueries : IVoucherQueries
{
    private readonly IVoucherRepository _voucherRepository;

    public VoucherQueries(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<VoucherDTO> ObterVoucherPorCodigo(string codigo)
    {
        var voucher = await _voucherRepository.ObterVoucherPorCodigo(codigo);

        if (voucher == null) return null;

        if (!voucher.EstaValidoParaUtilizacao()) return null;

        return new VoucherDTO
        {
            Codigo = voucher.Codigo,
            TipoDesconto = (int)voucher.TipoDesconto,
            Percentual = voucher.Percentual,
            ValorDesconto = voucher.ValorDesconto
        };

    }
}
